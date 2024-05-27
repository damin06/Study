#pragma once

// ��ġ(�ϵ����)�� ���� �����ϱ� ���� �Լ����� ����

class Device
{	
public:
	void Init();

	ComPtr<IDXGIFactory> GetDXGI() { return _dxgi; }
	ComPtr<ID3D12Device> GetDevice() { return _device; }

private:

	// COM�̶� �����ΰ�? ������Ʈ ������Ʈ ��
	// DX ���α׷��� ����� �������� ���� ȣȯ���� �����ϰ� �ϴ� ���
	// COM��ü(=COM�������̽�)�� ����Ͽ� ���λ��׵��� �츮���� ������
	// ComPtr = ������ ����Ʈ �����ͷ� �����Ͽ� ��� �����ϴ�

	ComPtr<ID3D12Debug>			_debugController;
	ComPtr<IDXGIFactory>		_dxgi; // ȭ�� ���� ��ɵ�
	ComPtr<ID3D12Device>		_device; // ���� ��ü ����
};

