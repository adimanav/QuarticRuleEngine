# Quartic Rule Engine

Rule Engine to verify input data according to predefined rules.


## Prerequisites

.NET Core SDK 2.0 on Linux/MacOS/Windows.

## About the Code

* QuarticRuleEngine directory contains code for the class library in assembly QuarticRuleEngine.dll, and QuarticRuleEngineApp directory contains code for the test application which references the QuarticRuleEngine.dll assembly.
* RulesEngine is the class of entry to the QuarticRuleEngine.dll assembly class library. RulesEngine instantiates RulesManager. RulesManager keeps an in memory repository of all the rules. Rules can be added, refreshed and obtained as needed.
* RulesManager uses RulesParser to parse the rules file and populate itself. RulesEngine then uses InputFileParser to get the input data and runs the applicable rules on the data.
* Have used .NET Lambda expression trees to increase performance. RulesManager compiles and store a lambda expression for each rule and runs it on the data.
* More data types can be added if necessary. Any .NET data type can be added easily. More operators can also be added. One can get currently supported operators by looking at the ExpressionType .NET class.
* The runtime performance depends on the rules and will vary. Worst case O(number_of_data_items * number_of_rules_per_signal_per_type). Since I'm using dictionaries, lookup is constant time.
* As requested, I have restricted myself to one day's worth of effort. If I had more time, 
  * I would add unit tests and/or integration tests. Currently I've only done manual testing due to lack of time.
  * I would make the code more extensible.
  * I would serialize the precompiled lambda expressions to disk and fetch them on re-initialization. That time can be cut down if the rules are not modified.
  * I would improve how the program gets the rules database and also input data. Maybe a GUI if needed.
  * Finally, and this is a big one, I would improve the algorithm by using either the Rete algorithm or Collection-Oriented Match, both complicated algorithms but run orders of magnitude faster.

## How to build and run

```
cd QuarticRuleEngineApp
dotnet clean
dotnet run
```
## How to add/remove rules

```
cd QuarticRuleEngineApp
vi RulesCollection.xml
dotnet clean
dotnet run
```

## How to change input JSON data

```
cd QuarticRuleEngineApp
vi raw_data.json
dotnet clean
dotnet run
```
