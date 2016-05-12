# Multiple sagas workflow example
## Instuctions
This sample includes both a v5 and v6 implementation

1. Download the sample.
2. Create a local database in sql server called `MultipleSaga`
3. Set the solution to run `Host`, `Dispatcher` and `Dispatcher2`

_Note: If you don't delete the queues between each run you may get some messages that can't reach the saga. This is due to the customer ids being randomly generated each time. It's nothing to worry about_
