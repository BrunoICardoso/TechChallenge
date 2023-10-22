Feature: Product

Scenario: Add a new Product
	Given I want to add a product with the following data
	| Field       | Value                                    |
	| Name        | Burger Picanha                           |
	| Category    | Lanche                                   |
	| Description | Hamburguer de picanha com molho especial |
	| Price       | 50                                       |
	When I request to add the product
	Then The product should be added