#include "pch.h"
#include "Engine.h"

void Engine::Init(const WindowInfo& wInfo)
{
    _window = wInfo;
    ResizeWindow(wInfo.width, wInfo.height);

    // �׷��� ȭ�� ũ�⸦ ����
    _viewport = { 0, 0, static_cast<FLOAT>(wInfo.width), static_cast<FLOAT>(wInfo.height), 0.0f, 1.0f };
    _scissorRect = CD3DX12_RECT(0, 0, wInfo.width, wInfo.height);

    //���� ��ġ�ʱ�ȭ �������� �޸𸮸� �Ҵ��Ѵ�.
    _device = make_shared<Device>();
    _cmdQueue = make_shared<CommandQueue>();
    _swapChain = make_shared<SwapChain>();
    _descHeap = make_shared<DescriptorHeap>();
    _rootSignature = make_shared<RootSignature>();
    _cb = make_shared<ConstantBuffer>();
    _tableDescHeap = make_shared<TableDescriptorHeap>();

    _device->Init();
    _cmdQueue->Init(_device->GetDevice(), _swapChain, _descHeap);
    _swapChain->Init(wInfo, _device->GetDXGI(), _cmdQueue->GetCmdQueue());
    _descHeap->Init(_device->GetDevice(), _swapChain);
    _rootSignature->Init();
    _cb->Init(sizeof(Transform), 256);
    _tableDescHeap->Init(256);
}

void Engine::Render()
{
    RenderBegin();

    RenderEnd();
}

void Engine::RenderBegin()
{
    _cmdQueue->RenderBegin(&_viewport, &_scissorRect);
}

void Engine::RenderEnd()
{
    _cmdQueue->RenderEnd();
}

void Engine::ResizeWindow(int32 width, int32 height)
{
    _window.width = width;
    _window.height = height;

    RECT rect = { 0, 0, width, height };
    ::AdjustWindowRect(&rect, WS_OVERLAPPEDWINDOW, false);
    ::SetWindowPos(_window.hwnd, 0, 100, 100, width, height, 0);

    // :: <- ���� Ȯ�� ������
}
