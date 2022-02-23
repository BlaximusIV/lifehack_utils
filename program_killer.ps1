# In order to run this script, you'll have to enable running scripts
# E.g. `Set-ExecutionPolicy -ExecutionPolicy [WhateverPolicyYouWantThatAllowsExecution]`

# The idea is to have this script run by the system's task scheduler to enforce acceptable hours 
# of operation for the given programs

# An Example run could be .\program_killer.ps1 -Programs 'steam,edge,netflix' -MinHour 5 -MaxHour 21
# This would shut down Steam, Microsoft Edge, and Netflix if the script was run before 5am or at/after 10pm

# the programs to kill, the minimum hour of operation (inclusive), the maximum hour of operation (exclusive), on a 24 hr clock
param([String]$Programs, [Int32]$MinHour, [Int32]$MaxHour)

# if it's outside acceptable hour of operation
$hour = (Get-Date).Hour;
if ($hour -lt $MinHour -or $hour -gt $MaxHour) {
    # kill the programs with the specified, comma delimited names
    foreach ($program in $Programs.Split(',')) {
        Get-Process *$program* | Stop-Process
    }
}