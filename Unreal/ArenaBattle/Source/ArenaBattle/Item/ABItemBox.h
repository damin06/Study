// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Actor.h"
#include "ABItemBox.generated.h"

UCLASS()
class ARENABATTLE_API AABItemBox : public AActor
{
	GENERATED_BODY()
	
public:	
	// Sets default values for this actor's properties
	AABItemBox();

public:
    UFUNCTION()
    void OnOverlapBegin(UPrimitiveComponent* OverlappedComponent, AActor* OtherActor, UPrimitiveComponent* OtherComp, int32 OtherBodyIndex, bool bFromSweep, const FHitResult& SweepResult);

    UFUNCTION()
    void OnEffectFinished(class UParticleSystemComponent* ParticleSystem);

protected:
    UPROPERTY(VisibleAnywhere, Category = Box)
    TObjectPtr<class UBoxComponent> Trigger;

    UPROPERTY(VisibleAnywhere, Category = Box)
    TObjectPtr<class UStaticMeshComponent> Mesh;

    UPROPERTY(VisibleAnywhere, Category = Box)
    TObjectPtr<class UParticleSystemComponent> Effect;
};
