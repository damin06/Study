#pragma once
class Mesh
{
public:
    void Init(vector<Vertex>& vec);
    void Render();

private:
    ComPtr<ID3D12Resource>        _vertexBuffer;
    D3D12_VERTEX_BUFFER_VIEW    _vertexBufferView = {};
    uint32 _vertexCount = 0;
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

