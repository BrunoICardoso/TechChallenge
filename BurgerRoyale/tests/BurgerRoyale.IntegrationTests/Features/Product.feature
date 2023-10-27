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

Scenario: Get products by category
	Given I have a product added with category "Lanche"
	When I get products given the category "Lanche"
	Then I should only see the products with "Lanche" category

Scenario: Get product
	Given I have a product added with category "Lanche"
	When I get this product
	Then I should only see the product

Scenario: Update a product
	Given I have a product added with category "Lanche"
	When I update this product with the following data
		| Field       | Value                       |
		| Name        | Pudim de chocolate especial |
		| Category    | Sobremesa                   |
		| Description | Delicioso pudim             |
		| Price       | 35                          |
	Then the product should be updated

Scenario: Delete a product
	Given I have a product added with category "Lanche"
	When I delete this product
	Then the product should be deleted