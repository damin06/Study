#include "pch.h"
#include "Game.h"
#include "Engine.h"

shared_ptr<Mesh> mesh = make_shared<Mesh>();
shared_ptr<Shader> shader = make_shared<Shader>();

void Game::Init(const WindowInfo& wInfo)
{
	GEngine->Init(wInfo);

	shader->Init(L"..\\Rerources\\Shader\\default.hlsli");
	
	vector<Vertex> Vertices = {
		{ Vec3(0.0f, 0.5f, 0.0f), Vec4(1.0f, 0.0f, 0.0f, 1.0f)},
		{ Vec3(0.5f, -0.5f, 0.0f), Vec4(0.0f, 1.0f, 0.0f, 1.0f)},
		{ Vec3(-0.5f, -0.5f, 0.0f), Vec4(0.0f, 0.0f, 1.0f, 1.0f)}
	};

	mesh->Init(Vertices);

	GEngine->GetCmdQueue()->WaitSync();
}

void Game::Update()
{
	//GEngine->Render();

	GEngine->RenderBegin();

	shader->Update();
	mesh->Render();

	GEngine->RenderEnd();
}
