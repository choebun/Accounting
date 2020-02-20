﻿Feature: BudgetQuery
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Add two numbers
	Given I have entered 50 into the calculator
	And I have entered 70 into the calculator
	When I press add
	Then the result should be 120 on the screen


Scenario: Whole Month Query
	Given Amount in DB
		| YearMonth | Amount |
		|  202002   |  290   |
	And intput 20200201 20200229
	When Query
	Then it should show result 290.00


Scenario: Empty Query
	Given Amount in DB
		| YearMonth | Amount |
		|		    |        |
	And intput 20200201 20200229
	When Query
	Then it should show result 0.00


Scenario: One Day Query
	Given Amount in DB
		| YearMonth | Amount |
		|  202002   |  290   |
	And intput 20200201 20200201
	When Query
	Then it should show result 29.00

Scenario: Cross Month Query
	Given Amount in DB
		| YearMonth | Amount |
		| 202002    | 290    |
		| 202003    | 300    |
	And intput 20200201 20200303
	When Query
	Then it should show result 380.00