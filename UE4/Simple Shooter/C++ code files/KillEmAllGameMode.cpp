// Fill out your copyright notice in the Description page of Project Settings.


#include "KillEmAllGameMode.h"
#include "EngineUtils.h"
#include "GameFramework/Controller.h"
#include "ShooterAIController.h"
#include "Kismet/GameplayStatics.h"


void AKillEmAllGameMode::BeginPlay() 
{
    for(AShooterAIController* AIController : TActorRange<AShooterAIController>(GetWorld())){
        EnemyRemainNum++;
    }
}


void AKillEmAllGameMode::PawnKilled(APawn* PawnKilled) 
{
    Super::PawnKilled(PawnKilled);
    // UE_LOG(LogTemp, Warning, TEXT("%s is killed"), *PawnKilled->GetName());

    APlayerController* PlayerController = Cast<APlayerController>(PawnKilled->GetController());
    if(PlayerController){
        EndGame(false);
        return;
    }
    
    else{         
        EnemyRemainNum--;
        UE_LOG(LogTemp, Warning, TEXT("EnemyRemainNum is %d"), EnemyRemainNum);

        for(AShooterAIController* AIController : TActorRange<AShooterAIController>(GetWorld())){
            if(!AIController->IsDead()){

                return;
            }
        }
    }

    EndGame(true);
    return;

}

int AKillEmAllGameMode::GetEnemyRemainNum() const
{
    return EnemyRemainNum;
}


void AKillEmAllGameMode::EndGame(bool bIsPlayerWinner) 
{
    for (AController* Controller : TActorRange<AController>(GetWorld())){
        bool bIsWinner = Controller->IsPlayerController() == bIsPlayerWinner;
        Controller->GameHasEnded(Controller->GetPawn(), bIsWinner);
    }
}


