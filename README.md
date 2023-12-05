# Coding test .NET
This is the first part of the test, simulating an ATM Machine.

## Requests

- Should have a list of bills that is allowed to the customer draw. In this example, we should use 10.00, 50.00, and 100.00 money bills;
- It should return the combination of money bills, using bills that was set at the beginning of the program;
- It should wait for the user Inputs the amount of cash to draw;

## Used approach

The easiest way to make it work is to make the method that calculates the return possibilities works recursivelly.
So, every call of this method, should check:
- First: If the amount of withdraw is already reached;
- Second: Check if the amount passed the limit;
- Third: Checks the denomination, related to the current denomination, and for every denomination, the method should be called and checked again (the first and second step);