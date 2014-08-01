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
	And the candidate has the following resumes
	| FileName       |
	| Resume.pdf |
	When I submit the the candidate to AddCandidate
	Then I should be redirected to the Position Candidates Page
	And the requested position should have a status of 'Open'
	And 'Fred Bob' should be listed as a candidate with a status of 'Resume Received'
	And the candidate details page for 'Fred Bob' should show the following resumes
	| Title      |
	| Resume.pdf |

Scenario: Add Candidate after position has been filled
	Given I have added the following candidate
	| Field | Value        |
	| Name  | Fred Bob     |
	| Email | fred@bob.com |
	| Phone | 123-456-7890 |
	And I have hired the candidate 'Fred Bob'
	When I add the following candidate
	| Field | Value        |
	| Name  | Sue Jane     |
	| Email | sue@jane.com |
	| Phone | 123-456-7890 |
	Then I should be returned to the Add Candidate page
	And the page should report the following errors
	| PropertyName | Message                                                  |
	| PositionId   | Cannot add a candidate to a filled position. |


