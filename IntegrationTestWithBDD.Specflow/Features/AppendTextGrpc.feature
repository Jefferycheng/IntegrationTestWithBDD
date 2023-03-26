Feature: AppendText
	

Scenario: Given a text, and append some text behind the original text. 
	Given a text "I am apple"
	When append the text
	Then the result text should be "I am apple this is appended text"
	
Scenario: Mock append text service;given a text, and append some text behind the original text.
	Given a text "I am apple"
	And using mock append text service	
	When append the text
	Then the result text should be "I am apple this function has been change to mock"