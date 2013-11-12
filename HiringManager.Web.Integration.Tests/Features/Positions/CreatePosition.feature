Feature: CreatePosition
	In order to manage my staffing needs
	As a manager
	I want to create an open position for my organization

Scenario: Create Position
	Given I want to create the position 'Senior Software Developer' to start on 'June 1, 2011'
	When I submit the create position request
	Then the I should be redirected to the Position Index page
	And the requested position should be listed on the Position Index Page
	And the requested position should have a status of 'Open'

Scenario: Add Candidate
	Given I have created the position 'Senior Software Developer' to start on 'June 1, 2011'
	When I receive resumes from the following candidates
	| Name     | EmailAddress | PhoneNumber  |
	| Fred Bob | fred@bob.com | 555-123-1234 |
	| Bob Fred | bob@fred.com | 555-234-1231 |
	Then the requested position should have a 2 candidate(s) awaiting review count
	And the position details should contain the following candidates
	| Name     | EmailAddress | PhoneNumber  | Status          |
	| Fred Bob | fred@bob.com | 555-123-1234 | Resume Received |
	| Bob Fred | bob@fred.com | 555-234-1231 | Resume Received |

Scenario: Set Candidate Status
	Given I have created the position 'Senior Software Developer' to start on 'June 1, 2011'
	And I have received resumes from the following candidates
	| Name     | EmailAddress | PhoneNumber  |
	| Fred Bob | fred@bob.com | 555-123-1234 |
	| Bob Fred | bob@fred.com | 555-234-1231 |
	When I set the candidate status for 'Fred Bob' to 'Phone Screened'
	Then the requested position should have a 2 candidate(s) awaiting review count
	And the position details should contain the following candidates
	| Name     | EmailAddress | PhoneNumber  | Status          |
	| Fred Bob | fred@bob.com | 555-123-1234 | Phone Screened  |
	| Bob Fred | bob@fred.com | 555-234-1231 | Resume Received |

Scenario: Pass on a Candidate
	Given I have created the position 'Senior Software Developer' to start on 'June 1, 2011'
	And I have received resumes from the following candidates
	| Name     | EmailAddress | PhoneNumber  |
	| Fred Bob | fred@bob.com | 555-123-1234 |
	| Bob Fred | bob@fred.com | 555-234-1231 |
	When I pass on the candidate 'Fred Bob'
	Then the requested position should have a 1 candidate(s) awaiting review count
	And the position details should contain the following candidates
	| Name     | EmailAddress | PhoneNumber  | Status          |
	| Fred Bob | fred@bob.com | 555-123-1234 | Passed          |
	| Bob Fred | bob@fred.com | 555-234-1231 | Resume Received |
