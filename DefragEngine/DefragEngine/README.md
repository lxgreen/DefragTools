# DefragTools
MS Defrag Tools series inspired "swiss knife" application

## DefragEngine
DefragEngine is an infrastructure for tool Bundle creation and management. Tools can be attached to a Category, and Categories are combined into Bundle.

Tool can be deployed to local system, invoked, updated, configured, and removed from local system.

It is also possible to chain Tools, and save these chains as DefragTool sessions. The chaining can be "parallel" when multiple Tools run either simultaneously and independently, or it can be "serial" when the output of the first Tool is the input of the next one. The latter mode fits more to CLI tools.