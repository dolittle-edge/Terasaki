Feature: TerasakiLineHandler

    Background: Given handler
        Given a handler of type TerasakiLineHandler

    Scenario: Handling incoming Terasaki sentence
        When the following events of type TcpLineReceived is produced
            | sentence                 |
            | 001003,123, 456, 789,*33 |
        Then the following events of type TerasakiDatapointOutput is produced
            | Source   | Tag   | Value |
            | Terasaki | 001:1 | 123   |
            | Terasaki | 001:2 | 456   |
            | Terasaki | 001:3 | 789   |
