Feature: AddCandidate
	In order to manage my staffing needs
	As a manager
	I want to add candidates to open positions

Background: 
	Given I want to create the position 'Senior Software Developer' to start on 'June 1, 2011'
	And I submit the create position request

Scenario: Add Candidate
	Given I have the following candidate
	| Field | Value        |
	| Name  | Fred Bob     |
	| Email | fred@bob.com |
	| Phone | 123-456-7890 |
	When I submit the the candidate to AddCandidate
	Then I should be redirected to the Position Candidates Page
	And the requested position should have a status of 'Open'
	And 'Fred Bob' should be listed as a candidate with a status of 'Resume Received'
