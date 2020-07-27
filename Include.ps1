function Exec
{
    [CmdletBinding()]
    param(
        [Parameter(Position=0,Mandatory=1)][scriptblock]$cmd,
        [Parameter(Position=1,Mandatory=0)][string]$errorMessage = ("Error executing command {0}" -f $cmd)
    )
    & $cmd
    if ($lastexitcode -ne 0) {
        throw ("Exec: " + $errorMessage)
    }
}

function Compose
{
    $compose_cmd = "docker-compose -f docker-compose.yml "
    $down_cmd = "down --remove-orphans --volumes"
    $up_cmd = "up --force-recreate --build "

    Exec {
        Invoke-Expression "$compose_cmd $down_cmd"
    } "Failed to run compose: $compose_cmd $down_cmd"

    Exec {
        Invoke-Expression "$compose_cmd $up_cmd"
    } "Failed to run compose: $compose_cmd $up_cmd"
}