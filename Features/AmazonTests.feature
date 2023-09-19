Feature: Amazon test feature
Testing the search for item and add item to basket

@AmazonTest
Scenario: Search for item and add to basket
	Given Open the browser
	When Enter the page URL
	Then Verify the correct page is opened

	Given Click on all categories
	And Choose a category
	When Search for book
	Then Verify the book exist
	And Choose the book
	And Verify the title of the book is correct
	And Verify the price of the book
	And Verify the type of the book

	When Add the book to the basket
	Then Verify that the right book is added to the basket
	And Verify that one item is in the basket
	When Going to the basket
	Then Verify book is showned
	And Verify the type, price, quantity and total price