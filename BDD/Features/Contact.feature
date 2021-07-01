Feature: Contact

Background: Customer tries to Contact us
	Given A Customer tries to contact us

@Contact
Scenario: Verify display of Error Message for Invalid Email id and Invalid Telephone on Contact Page	
	When Customer enters invalid Email id and telephone
	Then error message for invalid email and invalid telephone is displayed

@Contact
Scenario: Verify display of mandatory field error messages on Contact Page
	When Customer submits empty information
	Then Errors for mandatory information are displayed

@Contact
Scenario: Verify no error messages if customer enters mandatory information on Contact Page
	When Customer submits all mandatory information
		| ForeName    | Email                | Telephone      | Message              |
		| TestUsertwo | Testusertwo@test.com | 04 124 110 222 | Testuser message two |
	Then No Error messages for mandatory information are displayed

@Contact
Scenario: Ensure Acknowledgement message is displayed when customer submits mandatory information on Contact Page
	When Customer submits all mandatory information
		| ForeName    | Email                | Telephone      | Message            |
		| TestUserone | Testuserone@test.com | 04 124 110 110 | Testuser message 1 |
	Then Acknowledgement message is displayed