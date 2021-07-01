Feature: Cart

@cart
Scenario: Buy products of varying quantities
	Given a customer adds following items to the cart
		| ProductName    | Quantity |
		| Stuffed Frog   | 2        |
		| Fluffy Bunny   | 5        |
		| Valentine Bear | 3        |
	Then sub total should be correct for all items
	And total price of items should be correct