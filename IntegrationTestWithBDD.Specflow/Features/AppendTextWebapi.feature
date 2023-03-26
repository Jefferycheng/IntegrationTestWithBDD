Feature: AppendText for web api
	

Scenario: Given a text, and append some text behind the original text. 
    Given api- a text "I am apple" 
    When api- append the text 
    Then api- the result text should be "I am apple this is appended text"