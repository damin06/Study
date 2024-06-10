#include "pch.h"
#include "Game.h"
#include "Engine.h"

shared_ptr<Mesh> mesh = make_shared<Mesh>();
shared_ptr<Shader> shader = make_shared<Shader>();
shared_ptr<Texture> texture = make_shared<Texture>();

void Game::Init(const WindowInfo& wInfo)
{
	GEngine->Init(wInfo);

	// �ﰢ�� ���� �׽�Ʈ �ڵ�
	//vector<Vertex> vec(3);

	//vec[0].pos = Vec3(0.f, 0.5f, 0.5f);
	//vec[0].color = Vec4(1.f, 0.f, 0.f, 1.f);

	//vec[1].pos = Vec3(0.5f, -0.5f, 0.5f);
	//vec[1].color = Vec4(0.f, 1.f, 0.f, 1.f);

	//vec[2].pos = Vec3(-0.5f, -0.5f, 0.5f);
	//vec[2].color = Vec4(0.f, 0.f, 1.f, 1.f);

	// VBV ���ؽ� 6���� �簢�� �׸��� �ڵ�
	//vector<Vertex> vec(6);
	//vec[0].pos = Vec3(-0.5f, 0.5f, 0.5f);
	//vec[0].color = Vec4(1.f, 0.f, 0.f, 1.f);
	//vec[1].pos = Vec3(0.5f, 0.5f, 0.5f);
	//vec[1].color = Vec4(0.f, 1.f, 0.f, 1.f);
	//vec[2].pos = Vec3(0.5f, -0.5f, 0.5f);
	//vec[2].color = Vec4(0.f, 0.f, 1.f, 1.f);

	//vec[3].pos = Vec3(0.5f, -0.5f, 0.5f);
	//vec[3].color = Vec4(0.f, 0.f, 1.f, 1.f);
	//vec[4].pos = Vec3(-0.5f, -0.5f, 0.5f);
	//vec[4].color = Vec4(0.f, 1.f, 0.f, 1.f);
	//vec[5].pos = Vec3(-0.5f, 0.5f, 0.5f);
	//vec[5].color = Vec4(1.f, 0.f, 0.f, 1.f);

	// VBV + IBV�� �簢�� �׷����� �ڵ�
	vector<Vertex> vec(4);
	vec[0].pos = Vec3(-0.5f, 0.5f, 0.5f);
	vec[0].color = Vec4(1.f, 0.f, 0.f, 1.f);
	vec[0].uv = Vec2(0.f, 0.f);

	vec[1].pos = Vec3(0.5f, 0.5f, 0.5f);
	vec[1].color = Vec4(0.f, 1.f, 0.f, 1.f);
	vec[1].uv = Vec2(1.f, 0.f);

	vec[2].pos = Vec3(0.5f, -0.5f, 0.5f);
	vec[2].color = Vec4(0.f, 0.f, 1.f, 1.f);
	vec[2].uv = Vec2(1.f, 1.f);

	vec[3].pos = Vec3(-0.5f, -0.5f, 0.5f);
	vec[3].color = Vec4(0.f, 1.f, 0.f, 1.f);
	vec[3].uv = Vec2(0.f, 1.f);

	vector<uint32> indexVec;
	{
		indexVec.push_back(0);
		indexVec.push_back(1);
		indexVec.push_back(2);
	}
	{
		indexVec.push_back(0);
		indexVec.push_back(2);
		indexVec.push_back(3);
	}

	mesh->Init(vec, indexVec);

	shader->Init(L"..\\Resources\\Shader\\default.hlsli");

	texture->Init(L"..\\Resources\\Texture\\defaultTex.jpg");

	GEngine->GetCmdQueue()->WaitSync();
}

void Game::Update()
{
	GEngine->Update();

	GEngine->RenderBegin();

	shader->Update();

	/*mesh->Render();*/

	//// �ﰢ�� 1
	//{
	//	Transform t;
	//	t.offset = Vec4(0.75f, 0.f, 0.f, 0.f);
	//	mesh->SetTransform(t);
	//	mesh->Render();
	//}

	//// �ﰢ�� 2
	//{
	//	Transform t;
	//	t.offset = Vec4(0.f, 0.75f, 0.f, 0.f);
	//	mesh->SetTransform(t);
	//	mesh->Render();
	//}

	//// VBV�� ����� �簢��
	//{
	//	Transform t;
	//	t.offset = Vec4(0.f, 0.f, 0.2f, 0.f);
	//	mesh->SetTransform(t);

	//	mesh->SetTexture(texture);

	//	mesh->Render();
	//}

	//// VBV�� ����� �簢��
	//{
	//	Transform t;
	//	t.offset = Vec4(0.25f, 0.25f, 0.f, 0.f);
	//	mesh->SetTransform(t);

	//	mesh->SetTexture(texture);

	//	mesh->Render();
	//}

	
	// Ű���� �Է� �׽�Ʈ
	{
		static Transform t = {};

		if (INPUT->GetButton(KEY_TYPE::W))
			t.offset.y += 1.f * DELTA_TIME;

		if (INPUT->GetButton(KEY_TYPE::S))
			t.offset.y -= 1.f * DELTA_TIME;

		if (INPUT->GetButton(KEY_TYPE::A))
			t.offset.x -= 1.f * DELTA_TIME;

		if (INPUT->GetButton(KEY_TYPE::D))
			t.offset.x += 1.f * DELTA_TIME;

		mesh->SetTransform(t);

		mesh->SetTexture(texture);

		mesh->Render();
	}

	{
		static Transform a;
		a.offset = Vec4(0.25f, 0.25f, 0.f, 0.f);

		if (INPUT->GetButton(KEY_TYPE::SPACE))
			a.offset.z -= DELTA_TIME;
		else
			a.offset.z += DELTA_TIME;

		mesh->SetTransform(a);

		mesh->SetTexture(texture);

		mesh->Render();
	}

	GEngine->RenderEnd();
}
