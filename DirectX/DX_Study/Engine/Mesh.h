#pragma once
class Mesh
{
public:
    void Init(const vector<Vertex>& vertexBuffer, const vector<uint32>& indexBuffer);
    void Render();

    void SetTransform(const Transform& t) { _transform = t; }

private:
    void CreateVertexBuffer(const vector<Vertex>& buffer);
    void CreateIndexBuffer(const vector<uint32>& buffer);

private:
    // �ε��� ���� ����
    ComPtr<ID3D12Resource>        _indexBuffer;
    D3D12_INDEX_BUFFER_VIEW        _indexBufferView;
    uint32 _indexCount = 0;

    ComPtr<ID3D12Resource>        _vertexBuffer;
    D3D12_VERTEX_BUFFER_VIEW    _vertexBufferView = {};
    uint32 _vertexCount = 0;

    Transform _transform = {};
};


/*
* 
* 1. ���ؽ�
* �ϳ��� ���̴�
* �����̶�� �Ѵ�
* 3D�� ���� �⺻ �����̴�
* 2D���� �ִ� ����Ʈ
* ������ ��ġ, ����, ���� �� �پ��� ������ ��� �ִ�
* 
* 2. �������̶�?
*  �ּ��� �� ����
* ���������� �𿩼� ���̵ȴ� -> �ﰢ��
*  ���ؽ� 3���� ���̸� �ϳ��� �� ������ �̷�� ������
* 
* 3. �޽���?
* 
* ��������� ���������� ������ �𿩼� ���� ��ü���(������Ʈ)
*/

