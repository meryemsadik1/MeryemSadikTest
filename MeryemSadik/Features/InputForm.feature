Feature: InputForm


    Scenario: The system shall return output results
    Given Given The user opens [https://demoqa.com/text-box] in the web browser
    When the user fill in the Full Name with [test user]
	And fill in the Email with [test@blabla.com]
    And fill in the Current Address with [C. Dobla, 5, 28054 Madrid, Spain]
	And fill in the Permanent Address with [Street X, 28013 Madrid, Spain]
	And the User press the [Submit] button
    Then the user can see the output results with the correct data sent
	
	
	
    Scenario: The system should return output results
    Given Given The user opens [https://demoqa.com/text-box] in the web browser
    When the user fill in the Full Name with [John Smith]
	And fill in the Email with [john.smith@mailinator.com]
    And fill in the Current Address with [Street Smith 3, London, UK]
	And fill in the Permanent Address with [Street Smith 6, London,UK]
	And the User press the [Submit] button
    Then the user can see the output results with the correct data
	
	
	Scenario: The system shall return an email error message
    Given Given The user opens [https://demoqa.com/text-box] in the web browser
	When the user fill in the Email with [Thisisnotanemail]
	And the User press the [Submit] button
    Then an error red indicator appears in the Email texbox 
	
	Scenario: The system shall not accept special characters
    Given Given The user opens [https://demoqa.com/text-box] in the web browser
	When the user fill in the Full Name with [Errortesttname$&]
	And the User press the [Submit] button
    Then an alert error will appear 
	
	Scenario: The system shall return the required field error
    Given Given The user opens [https://demoqa.com/text-box] in the web browser
	When the user leaves the Full name in blank
	And the User press the [Submit] button
    Then a required field alert error will appear 
