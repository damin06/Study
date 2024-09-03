#include "pch.h"
#include "CommandQueue.h"
#include "SwapChain.h"
#include "DescriptorHeap.h"
#include "Engine.h"


CommandQueue::~CommandQueue()
{
	::CloseHandle(_fenceEvent);
}

void CommandQueue::Init(ComPtr<ID3D12Device> device, shared_ptr<SwapChain> swapChain, shared_ptr<DescriptorHeap> descHeap)
{
	_swapChain = swapChain;
	_descHeap = descHeap;

	D3D12_COMMAND_QUEUE_DESC queueDesc = {};
	queueDesc.Type = D3D12_COMMAND_LIST_TYPE_DIRECT;
	queueDesc.Flags = D3D12_COMMAND_QUEUE_FLAG_NONE;

	// Ŀ�ǵ�ť�� �Լ��� ���� �����Ѵ�
	device->CreateCommandQueue(&queueDesc, IID_PPV_ARGS(&_cmdQueue));
	// - D3D12_COMMAND_LIST_TYPE_DIRECT:  GPU�� ���� �����ϴ� ���� ���
	device->CreateCommandAllocator(D3D12_COMMAND_LIST_TYPE_DIRECT, IID_PPV_ARGS(&_cmdAlloc));

	// Ŀ�ǵ� ����Ʈ�� ���°��� CLOSE / OPEN(�����ٰ� �����ٰ� �ݺ�)
	// OPEN���¿��� COMMAND�� �־��ٰ� close�� ������ �����Ѵٴ� ����
	device->CreateCommandList(0, D3D12_COMMAND_LIST_TYPE_DIRECT, _cmdAlloc.Get(), nullptr, IID_PPV_ARGS(&_cmdList));

	_cmdList->Close();

	// ���ҽ� �ε�� Ŀ�ǵ� ����Ʈ ����
	device->CreateCommandAllocator(D3D12_COMMAND_LIST_TYPE_DIRECT, IID_PPV_ARGS(&_resCmdAlloc));
	device->CreateCommandList(0, D3D12_COMMAND_LIST_TYPE_DIRECT, _resCmdAlloc.Get(), nullptr, IID_PPV_ARGS(&_resCmdList));


	// CPU�� GPU�� ����ȭ �������� ����
	device->CreateFence(0, D3D12_FENCE_FLAG_NONE, IID_PPV_ARGS(&_fence));


	// ��Ƽ�����忡�� ����ȭ �� �� �ַ� ����ϴ� ���
	// �̺�Ʈ�� ��ȣ��� ���� ����
	// �������϶� �����ִٰ� �Ķ����϶� ���������� ���!(��ٸ���) -> ����ȭ ��Ű�� �뵵
	_fenceEvent = ::CreateEvent(nullptr, FALSE, FALSE, nullptr);
}

void CommandQueue::WaitSync()
{
	// GPU���� ���ָ� �� ������ �ε��� ����
	_fenceValue++;

	// ���� ��⿭�� ���ο� �潺����Ʈ�� �����ϴ� ������ �߰�
	// GPU Ÿ�Ӷ��ο� �����Ƿ� GPU�� �ϴ����� �Ϸ�ɶ����� �� ��Ÿ�� ������ �������� �ʴ´�

	_cmdQueue->Signal(_fence.Get(), _fenceValue);

	// GPU�� �� �潺�������� ������ �Ϸ��� ������ ��ٸ���
	if (_fence->GetCompletedValue() < _fenceValue)
	{
		// ���� �潺�� �����ϸ� �̺�Ʈ�� �߻���Ŵ
		// _fenceValue��ŭ�� ���� �� ���´ٸ� �̺�Ʈ�� �߻�(�Ķ��� ����)
		_fence->SetEventOnCompletion(_fenceValue, _fenceEvent);

		// GPU�� ���� �潺�� �����Ҷ����� ��ٸ�����. �� �̺�Ʈ�� ���۵�
		// �� CPU�� ��ٸ��� �ִ���......
		::WaitForSingleObject(_fenceEvent, INFINITE);
	}
}

void CommandQueue::RenderBegin(const D3D12_VIEWPORT* vp, const D3D12_RECT* rect)
{
	_cmdAlloc->Reset();
	_cmdList->Reset(_cmdAlloc.Get(), nullptr);

	// ����ü�ΰ� �¹����� �۵��ϴ� ���̴�
	D3D12_RESOURCE_BARRIER barrier = CD3DX12_RESOURCE_BARRIER::Transition(
		_swapChain->GetCurrentBackBufferResource().Get(),
		D3D12_RESOURCE_STATE_PRESENT, // ȭ�� ���
		D3D12_RESOURCE_STATE_RENDER_TARGET); // ���� �����

	_cmdList->SetGraphicsRootSignature(ROOT_SIGNATURE.Get());
	//GEngine->GetCB()->Clear();
	GEngine->GetConstantBuffer(CONSTANT_BUFFER_TYPE::TRANSFORM)->Clear();
	GEngine->GetConstantBuffer(CONSTANT_BUFFER_TYPE::MATERIAL)->Clear();

	GEngine->GetTableDescHeap()->Clear();

	ID3D12DescriptorHeap* descHeap = GEngine->GetTableDescHeap()->GetDescriptorHeap().Get();
	_cmdList->SetDescriptorHeaps(1, &descHeap);	// � ���� ����Ұ��� �������ش�

	_cmdList->ResourceBarrier(1, &barrier);

	// ����Ʈ�� ���� �簢���� �����Ѵ�
	// �̴� ���� ����� �缳�� �� ������ ȣ��Ǿ�߸� �Ѵ�
	_cmdList->RSSetViewports(1, vp);
	_cmdList->RSSetScissorRects(1, rect);

	// �������� ���۸� �����Ѵ�
	D3D12_CPU_DESCRIPTOR_HANDLE backBufferView = _descHeap->GetBackBufferView();
	_cmdList->ClearRenderTargetView(backBufferView, Colors::LightSteelBlue, 0, nullptr);
	_cmdList->OMSetRenderTargets(1, &backBufferView, FALSE, nullptr);

	// DSV�� �����
	D3D12_CPU_DESCRIPTOR_HANDLE depthStencilView = GEngine->GetDepthStencilBuffer()->GetDSVCpuHandle();
	_cmdList->OMSetRenderTargets(1, &backBufferView, FALSE, &depthStencilView);
	_cmdList->ClearDepthStencilView(depthStencilView, D3D12_CLEAR_FLAG_DEPTH, 1.0f, 0, 0, nullptr);
}

void CommandQueue::RenderEnd()
{
	D3D12_RESOURCE_BARRIER barrier = CD3DX12_RESOURCE_BARRIER::Transition(
		_swapChain->GetCurrentBackBufferResource().Get(),
		D3D12_RESOURCE_STATE_RENDER_TARGET,	// ���� �����
		D3D12_RESOURCE_STATE_PRESENT);	// ȭ�� ���

	_cmdList->ResourceBarrier(1, &barrier);
	_cmdList->Close();

	// Ŀ�ǵ� ����Ʈ ����
	ID3D12CommandList* cmdListArr[] = { _cmdList.Get() };
	_cmdQueue->ExecuteCommandLists(_countof(cmdListArr), cmdListArr);

	// ���� ȭ���� ������
	_swapChain->Present();

	WaitSync();

	_swapChain->SwapIndex();
}

void CommandQueue::FlushResourceCommandQueue()
{
	_resCmdList->Close();

	ID3D12CommandList* cmdListArr[] = { _resCmdList.Get() };
	_cmdQueue->ExecuteCommandLists(_countof(cmdListArr), cmdListArr);

	WaitSync();

	_resCmdAlloc->Reset();
	_resCmdList->Reset(_resCmdAlloc.Get(), nullptr);
}