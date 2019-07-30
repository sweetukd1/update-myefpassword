:: Credentials Path
CAlL D:\myef-security\myefdev.bat

echo off
echo Started Deployment ------------------------------------------
echo Setting variables -------------------------------------------
::jenkins artifacts path
set /p ARTIFACTS_PATH=<SOLUTION_FOLDER.txt
set ARTIFACTS_PATH=D:\%ARTIFACTS_PATH%


IF EXIST SOLUTION_FOLDER.txt DEL /F SOLUTION_FOLDER.txt

::Image Name, Service Name and Cluster Name
set CLUSTER_NAME=myefdev
set IMAGE_NAME=myefpassword

:: Schedule Timings in minutes - 15
set INTERVAL=15

ECHO.%JOB_NAME% | FIND /I "-DEV">Nul && ( SET FOLDER_NAME=DEV)
ECHO.%JOB_NAME% | FIND /I "-QA">Nul && ( SET FOLDER_NAME=QA)
ECHO.%JOB_NAME% | FIND /I "-LIVE">Nul && ( SET FOLDER_NAME=LIVE)

:: Docker build image and push to ECR
copy dockerfile %ARTIFACTS_PATH%\%FOLDER_NAME%\%BUILD_NUMBER%\dockerfile

:: ECR Repo. Create if not exist
echo AWS ECR Repo ---------------------------------------------
powershell.exe aws ecr describe-repositories --query 'repositories[*].repositoryName' --output text >aws-ecr-list.txt
set /p ECR_REPO_LIST=<aws-ecr-list.txt
echo.%ECR_REPO_LIST%|findstr /C:"%IMAGE_NAME%" >nul 2>&1
if not errorlevel 1 (
   echo Pushing docker image to existing ECR Repo
) else (
    echo ECR Repo does not exists. Creating new ECR Repo
	aws ecr create-repository --repository-name %IMAGE_NAME%
	aws ecr put-lifecycle-policy --repository-name "%IMAGE_NAME%" --lifecycle-policy-text "file://policy.json"
)
IF EXIST aws-ecr-list.txt DEL /F aws-ecr-list.txt
cd %ARTIFACTS_PATH%\%FOLDER_NAME%\%BUILD_NUMBER%
echo Pushing docker image to ECR Repo --------------------------------------------------
docker build --pull=true -t %IMAGE_NAME%:%BUILD_NUMBER% .
powershell.exe Invoke-Expression -Command (Get-ECRLoginCommand -Region %AWS_DEFAULT_REGION%).Command
docker tag %IMAGE_NAME%:%BUILD_NUMBER% %AWS_ACCOUNT_NO%.dkr.ecr.%AWS_DEFAULT_REGION%.amazonaws.com/%IMAGE_NAME%:%BUILD_NUMBER%
docker push %AWS_ACCOUNT_NO%.dkr.ecr.%AWS_DEFAULT_REGION%.amazonaws.com/%IMAGE_NAME%:%BUILD_NUMBER%
docker rmi -f %IMAGE_NAME%:%BUILD_NUMBER%
docker rmi -f %AWS_ACCOUNT_NO%.dkr.ecr.%AWS_DEFAULT_REGION%.amazonaws.com/%IMAGE_NAME%:%BUILD_NUMBER%

::Create ECS Task Definition
echo Updating ECS Task Definition --------------------------------------------------------
aws ecs register-task-definition --family %IMAGE_NAME% --container-definitions "[{\"name\":\"%IMAGE_NAME%\",\"image\":\"%AWS_ACCOUNT_NO%.dkr.ecr.%AWS_DEFAULT_REGION%.amazonaws.com/%IMAGE_NAME%:%BUILD_NUMBER%\",\"cpu\":200,\"memory\":500,\"essential\":true}]"
powershell.exe aws ecs describe-task-definition --task-definition %IMAGE_NAME% --query 'taskDefinition.revision' > get-revision.txt
set /p revision=<get-revision.txt

::Update AWS ECS Schedule Task
aws events put-rule --schedule-expression "rate(%INTERVAL% minutes)" --name %IMAGE_NAME% --description "Revision-%revision% Build-%BUILD_NUMBER%"

aws events put-targets --rule %IMAGE_NAME% --targets "Id"="%IMAGE_NAME%","Arn"="arn:aws:ecs:%AWS_DEFAULT_REGION%:%AWS_ACCOUNT_NO%:cluster/%CLUSTER_NAME%","RoleArn"="arn:aws:iam::%AWS_ACCOUNT_NO%:role/ecsEventsRole","EcsParameters"="{"TaskDefinitionArn"= "arn:aws:ecs:%AWS_DEFAULT_REGION%:%AWS_ACCOUNT_NO%:task-definition/%IMAGE_NAME%:%revision%","TaskCount"= 1}"

:: Printing values
echo Image Name - %IMAGE_NAME%:%BUILD_NUMBER%
echo Task Definition revision no - %IMAGE_NAME%:%revision%