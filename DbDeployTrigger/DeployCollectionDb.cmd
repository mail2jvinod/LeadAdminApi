echo off
set ConnectionString=%1

SqlPackage /Action:Publish /SourceFile:"../DbDeploy/Digiclove.Collect.MainDatabase.dacpac" /TargetConnectionString:%ConnectionString% /p:BlockOnPossibleDataLoss=false
