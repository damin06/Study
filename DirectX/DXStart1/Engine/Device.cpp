#include "pch.h"
#include "Device.h"

void Device::Init()
{
	// DXGI��?
	// DXGI(DirectX Graphics Infrastructure)
	// ��üȭ�� ��� ��ȯ
	// �����Ǵ� ���÷��� ��� ���� ��
	::CreateDXGIFactory(IID_PPV_ARGS(&_dxgi));


	// CreateDevice
	// ���÷��� �����(�׷��� ī��)�� ��Ÿ���� ��ü
	// padapter : null�� �����ϸ� �ý��� �⺻ ���÷��� �����
	// �̴ϸط��� : �������α׷��� �䱸�ϴ� �ּ� ��� ����(������ �׷��� ī�� �ɷ��� �� �ִ�)
	::D3D12CreateDevice(nullptr, D3D_FEATURE_LEVEL_11_0, IID_PPV_ARGS(&_device));
}
