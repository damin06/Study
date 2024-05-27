#pragma once
class Texture
{
	// CBV���� �ֵ�� �ٸ����� ��ũ�������� ����� ���ο� �������� �並 ������� �ݸ�
	// �ؽ�ó�� �ѹ��� �ε��� �ϰ� �並 �ϳ��� ����� ����ص� �ȴ�
	
public:
	void Init(const wstring& path);

	D3D12_CPU_DESCRIPTOR_HANDLE GetCpuHandle() { return _srvHandle; }

public:
	void CreateTexture(const wstring& path);
	void CreateView();

private:
	ScratchImage			 		_image;
	ComPtr<ID3D12Resource>			_tex2D;

	ComPtr<ID3D12DescriptorHeap>	_srvHeap;
	D3D12_CPU_DESCRIPTOR_HANDLE		_srvHandle;
};

