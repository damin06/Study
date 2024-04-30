// Fill out your copyright notice in the Description page of Project Settings.


#include "AI/BTService_Detect.h"
#include "ABAI.h"
#include "AIController.h"
#include "Interface/ABCharacterAIInterface.h"
//#include "BehaviorTree/Tex"

UBTService_Detect::UBTService_Detect()
{

}

void UBTService_Detect::TickNode(UBehaviorTreeComponent& OwnerComp, uint8* NodeMemory, float DeltaSeconds)
{
	Super::TickNode(OwnerComp, NodeMemory, DeltaSeconds);

	APawn* ControllingPawn = OwnerComp.GetAIOwner()->GetPawn();
	if (nullptr == ControllingPawn)
	{
		return;
	}

	FVector Center = ControllingPawn->GetActorLocation();
	UWorld* World = ControllingPawn->GetWorld();
	if (nullptr == World) 
	{
		return;	
	}

	IABCharacterAIInterface* AIPawn = Cast<IABCharacterAIInterface>(ControllingPawn);
	if (nullptr == AIPawn) 
	{
		return;
	}

	//float DetectRadius = AIPawn->GetAIDetectRange();
	//TArray<FOverlapResult> OverlapResults;
	//FCollisionQueryParams CollisionQueryParams(SCENE_QUERY_STAT(Detect, false, ControllingPawn));
	//bool bResult = World->


}
