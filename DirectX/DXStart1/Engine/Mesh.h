#pragma once

class Texture;

class Mesh
{
public:
	void Init(const vector<Vertex>& vertexBuffer, const vector<uint32>& indexBuffer);
	void Render();

	void SetTransform(const Transform& t) { _transform = t; }
	void SetTexture(shared_ptr<Texture> tex) { _tex = tex; }

private:
	void CreateVertexBuffer(const vector<Vertex>& buffer);
	void CreateIndexBuffer(const vector<uint32>& buffer);

private:
	// VBV ����
	ComPtr<ID3D12Resource>		_vertexBuffer;
	D3D12_VERTEX_BUFFER_VIEW	_vertexBufferView = {};
	uint32 _vertexCount = 0;

	// IBV ����
	ComPtr<ID3D12Resource>		_indexBuffer;
	D3D12_INDEX_BUFFER_VIEW		_indexBufferView;
	uint32 _indexCount = 0;

	Transform _transform = {};
	shared_ptr<Texture> _tex = {};
};



/*
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
* ���ؽ� 3���� ���̸� �ϳ��� �� ������ �̷�� ������
* 
* 3. �޽���?
* 
* ��������� ���������� ������ �𿩼� ���� ��ü���(������Ʈ)
* 
*/