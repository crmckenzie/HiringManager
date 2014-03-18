Feature: ClosePosition
	In order to give up on a search
	As a hiring manager
	I want to be able to close a position

Scenario: Navigate to the Close Position page
	Given I want to create the position 'Senior Software Developer' to start on 'June 1, 2011'
	And I submit the create position request
	When I navigate to the Close Position page for 'Senior Software Developer'
	Then the Close Position page should display
	| Field         | Value                     |
	| PositionTitle | Senior Software Developer |

Scenario: Close the Position
	Given I want to create the position 'Senior Software Developer' to start on 'June 1, 2011'
	And I submit the create position request
	When I close the position for 'Senior Software Developer'
	Then I should be redirected to the Position Index page
