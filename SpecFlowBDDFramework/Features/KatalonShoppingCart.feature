Feature: Katalon Shopping Cart

Scenario: Add and Remove items items from shopping cart
	Given I am on the website homepage
	When I add four random items to my cart
	And I view my cart
	Then I find total four items listed in my cart
	When I remove a product from the cart
    Then the cart should have one less product with count 4