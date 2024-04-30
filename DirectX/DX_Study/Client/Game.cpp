#include "pch.h"
#include "Game.h"
#include "Engine.h"

shared_ptr<Mesh> mesh = make_shared<Mesh>();
shared_ptr<Shader> shader = make_shared<Shader>();

void Game::Init(const WindowInfo& wInfo)
{
	GEngine->Init(wInfo);

	
	vector<Vertex> Vertices = {
		{ Vec3(-0.5f, 0.5f, 0.0f), Vec4(1.0f, 0.0f, 0.0f, 1.0f)},
		{ Vec3(0.5f, 0.5f, 0.0f), Vec4(0.0f, 1.0f, 0.0f, 1.0f)},
		{ Vec3(0.5f, -0.5f, 0.0f), Vec4(0.0f, 0.0f, 1.0f, 1.0f)},
		{ Vec3(-0.5f, -0.5f, 0.0f), Vec4(0.0f, 0.0f, 1.0f, 1.0f)}
	};

	vector<uint32> indexVec = {
		0,1,2,0,2,3
	};


	mesh->Init(Vertices, indexVec);

	shader->Init(L"..\\Rerources\\Shader\\default.hlsli");

	GEngine->GetCmdQueue()->WaitSync();
}

void Game::Update()
{
	//GEngine->Render();

	GEngine->RenderBegin();

	shader->Update();
	//mesh->Render();


	// »ï°¢Çü 1
	{
		Transform t;
		t.offset = Vec4(0.0f, 0.0f, 0.f, 0.f);
		mesh->SetTransform(t);
		mesh->Render();
	}

	//// »ï°¢Çü 2
	//{
	//	Transform t;
	//	t.offset = Vec4(0.f, 0.75f, 0.f, 0.f);
	//	mesh->SetTransform(t);
	//	mesh->Render();
	//}
	

	GEngine->RenderEnd();
}
