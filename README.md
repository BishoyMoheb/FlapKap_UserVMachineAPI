# FlapKap_UserVMachineAPI
It is a dotNET Core Web API solution that design a vending machine, allowing users with a “seller” role to add, update or remove products, while users with a “buyer” role can deposit coins into the machine and make purchases. The machine accept only 5, 10, 20, 50 and 100 cent coins.

This solution uses JwtSecurityTokenHandler for authenticating users and authorization of their roles. It also uses custom validation class for validation of accepted deposit coins.
