ExpressionBuilder
=================

ExpressionBuilder is a framework which answers the following question: how can I query data in such a way that I don't have to change my DAL/services every time I get a new requirement?.

In order to show how ExpressionBuilder solves this, let's assume we have Customer table, and we would like to get all customers with LastName starting with A, and Age greater than 25. The obvious pseudo-query will look like this: "LastName like 'A*' and Age > 25". This query can also be represented as the following tree of logical expressions:

Logical expression "And"

1.  Comparision expression "like"

    1.1. Parameter "LastName"
    
    1.2. Value "A*"
    
2.  Comparision expression ">"
  
    2.1. Parameter "Age"
    
    2.2. Value 25
    

Types of expessions
===================

ExpressionBuilder contains a serialiable model which allows you to build logical expression like the one shown above. Here are the building parts:

1) LeftValueExpression - this is basically a reference to a property or a field, like Customer.LastName

2) RightValueExpression - a constant, like "25" or "A*"

3) ComparisionExpression - compares LeftValueExpression with RightValueExpression

4) LogicalExpression - combines ComparisionExpression into a tree


Converters
==========

Once you have an expession, the next step will be to convert it to something more usefull. 

1) LinqExpressionConverter - converts an expression to a delegate.

2) StringExpressionConverter - converts an expression to a string. This converter can used as a base for converts which convert expression to languages like SQL.


Expression Builder
==================

The library also includes a reusable UI framework (WPF) for building expressions:

![](http://i3.codeplex.com/Download?ProjectName=expbuilder&DownloadId=320287)
