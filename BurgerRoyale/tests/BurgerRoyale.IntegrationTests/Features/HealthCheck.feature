Feature: HealthCheck

Scenario: Check the status of application
	Given I want to check the status of my application
	When I check the application status
	Then I should see the status as ok