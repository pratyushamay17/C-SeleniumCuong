Feature: Shopping
	In order to shop online
	A customer has to be able to view and buy products

@shop
Scenario Outline: Product Price
	Given A product with name <ProductName> is in the shop catalog
	Then Price should be <ProductPrice>

	Examples:
		| ProductName | ProductPrice |
		| Teddy Bear  | 12.99        |

@shop
Scenario Outline: Buy Product
	Given A customer buys the product with name <ProductName>
	Then The product with name <ProductName> must be added to the cart

	Examples:
		| ProductName    |
		| Valentine Bear |

@shop
Scenario: Buy all products with cost less than $10
	Given A customer buys all products cheaper than $10
	Then The products must be added to the cart

@shop
Scenario: Buy cheapest product
	Given A customer buys the cheapest product
	Then The product must be added to the cart