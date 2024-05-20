// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/GameModeBase.h"
//#include "Interface/ABGameModeInterface"
#include "ABGameMode.generated.h"

/**
 * 
 */
UCLASS()
class ARENABATTLE_API AABGameMode : public AGameModeBase
{
	GENERATED_BODY()
	
public:
	AABGameMode();

public:
	//virtual void OnPlayerScore(int32 NewPlayerScore) override;
	//virtual void OnPlayerDead() override;
	//virtual void ISGameCLeared() override;

public:
	UPROPERTY(VisibleInstanceOnly, BlueprintReadOnly, Category = Game)
	int32 ClearScore;

	UPROPERTY(VisibleInstanceOnly, BlueprintReadOnly, Category = Game)
	int32 CurrentScore;

	UPROPERTY(VisibleInstanceOnly, BlueprintReadOnly, Category = Game)
	int32 bIsCleared : 1;
};
