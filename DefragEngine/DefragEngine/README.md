# DefragTools
MS Defrag Tools series inspired "swiss knife" application

## DefragEngine
DefragEngine is an infrastructure for tool bundle creation and management. Tools are any Windows executable files like EXE, BAT, PowerShell scripts, etc. Tools can be attached to categories, and categories are combined into bundles.

Tool can be deployed to local system, invoked, updated, configured, and removed from local system.

It is also possible to chain tools, and save these chains as DefragTool sessions. The chaining can be "parallel" when multiple tools run either simultaneously and independently, or it can be "serial" when the output of the first tool is the input of the next one. The latter mode fits more to CLI tools; however, it is possible to determine what are the "output" and the "input" of the chained tools.
