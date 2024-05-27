#pragma once

class SwapChain;
class DescriptorHeap;

class CommandQueue
{
public:
	~CommandQueue();

	void Init(ComPtr<ID3D12Device> device, shared_ptr<SwapChain> swapChain, shared_ptr<DescriptorHeap> descHeap);
	void WaitSync();

	void RenderBegin(const D3D12_VIEWPORT* vp, const D3D12_RECT* rect);
	void RenderEnd();

	void FlushResourceCommandQueue();

	ComPtr<ID3D12CommandQueue> GetCmdQueue() { return _cmdQueue; }
	ComPtr<ID3D12GraphicsCommandList> GetCmdList() { return	_cmdList; }
	ComPtr<ID3D12GraphicsCommandList> GetResourceCmdList() { return	_resCmdList; }

private:
	// DX12 �������� ���� ����(DX12���� ó�� ����!)
	// ���ָ� ��û�Ҷ� �ϳ��� ��û�ϸ� ��ȿ����
	// "���� ���"�� �ϰ��� �ϳ��ϳ� ����ߴٰ� �ѹ濡 ��û�Ѵ�

	ComPtr<ID3D12CommandQueue>			_cmdQueue;
	ComPtr<ID3D12CommandAllocator>		_cmdAlloc;
	ComPtr<ID3D12GraphicsCommandList>	_cmdList;


	ComPtr<ID3D12CommandAllocator>		_resCmdAlloc;
	ComPtr<ID3D12GraphicsCommandList>	_resCmdList;


	// Fence : ��Ÿ��
	// CPU / GPU ���̿� ����ȭ�� ���� ������ ����
	ComPtr<ID3D12Fence>					_fence;
	uint32								_fenceValue = 0;
	HANDLE								_fenceEvent = INVALID_HANDLE_VALUE;

	shared_ptr<SwapChain>		_swapChain;
	shared_ptr<DescriptorHeap>	_descHeap;
};

