#pragma once

/// <summary>
/// ������ �ϴ���, GPU�� ���� �޸𸮿� � ���۸� ����ϰڴٸ� ����ϴ� ����� ���
/// </summary>
class RootSignature
{
public:
    void Init(ComPtr<ID3D12Device> device);

    ComPtr<ID3D12RootSignature>    GetSignature() { return _signature; }

private:
    ComPtr<ID3D12RootSignature>    _signature;
};

