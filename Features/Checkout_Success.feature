@Authorized_Customer @Test @Checkout @webQa
Feature: Happy Scenario Checkout Feature

  Scenario: Successful Checkout
    Given I add the product "Sauce Labs Bike Light" to my cart
    And I navigate to the Cart
    Then I click the Checkout button
    And I provide the required checkout data:
      | First Name  | Last Name | Postal Code |
      | John        | Doe       | 12345       |
    When I click the Finish button
    Then I should see the message "Thank you for your order!"