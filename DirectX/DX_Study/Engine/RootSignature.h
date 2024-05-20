#pragma once

/// <summary>
/// ������ �ϴ���, GPU�� ���� �޸𸮿� � ���۸� ����ϰڴٸ� ����ϴ� ����� ���
/// </summary>
class RootSignature
{
public:
    void Init();

    ComPtr<ID3D12RootSignature>    GetSignature() { return _signature; }

private:
    void CreateSamplerDesc();
    void CreateRootSignature();

private:
    ComPtr<ID3D12RootSignature>    _signature;

    D3D12_STATIC_SAMPLER_DESC _samplerDesc;
};