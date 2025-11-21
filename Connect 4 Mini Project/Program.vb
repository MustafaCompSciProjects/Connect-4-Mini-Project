Imports System
Imports Utilities

Module Program
    Const BOARD_WIDTH As Integer = 7
    Const BOARD_HEIGHT As Integer = 6
    Const NUMBER_OF_PLAYERS As Integer = 2


    Sub Main(args As String())


        Dim players(NUMBER_OF_PLAYERS - 1) As String
        Dim playerSymbols(NUMBER_OF_PLAYERS - 1) As Char

        GetNames(players)

        Console.Clear()
        Console.WriteLine($"Hello {players(0)} and {players(1)}, I am the Connect 4 program")

        System.Threading.Thread.Sleep(1000)

        Console.Clear()

        Dim mainMenuChoice As Integer

        'Get a valid option
        Do
            mainMenuChoice = GetIntInput($"Would you like to: {vbCrLf} 1. Play Game {vbCrLf} 2. Exit")
        Loop Until mainMenuChoice = 1 Or mainMenuChoice = 2

        'Carry out that option
        If mainMenuChoice = 1 Then
            PlayGame(players, playerSymbols)
        Else
            End
        End If

    End Sub

    Sub PickSymbols(playerSymbols() As Char)
        Dim numPlayerSymbols As Integer = playerSymbols.Length

        For i As Integer = 0 To numPlayerSymbols - 1
            Dim symbolChosen As String
            Dim validSymbol As Boolean = False

            Do
                symbolChosen = GetCharInput($"Player {i + 1}, what symbol do you want to use")

                If Not playerSymbols.Contains(symbolChosen) Then
                    validSymbol = True
                Else
                    Console.WriteLine("Please enter a unique character")

                End If
            Loop Until validSymbol

            playerSymbols(i) = symbolChosen
        Next

    End Sub

    Sub PlayGame(ByVal players() As String, ByRef playerSymbols() As Char)
        Dim gameBoard(BOARD_WIDTH, BOARD_HEIGHT) As String

        PickSymbols(playerSymbols)

        InitBoard(gameBoard)

        PrintBoard(gameBoard)

        Dim playing As Boolean = True

        While playing



        End While

    End Sub


    Sub GetNames(players())
        'Count number of players (defined in main)
        Dim numPlayers As Integer = players.Count - 1

        For i As Integer = 0 To numPlayers

            'Get player names for each of them
            players(i) = GetStrInput($"Whats player {i + 1}'s name?")

        Next
    End Sub

    Sub PrintBoard(ByRef board(,) As String)

        'Setup the top row of numbers
        Console.Write("  ")
        For i As Integer = 1 To BOARD_WIDTH
            Console.Write($"{i} ")
        Next
        Console.WriteLine()

        'Show the board with its row number first
        For i As Integer = 0 To BOARD_HEIGHT - 1
            Console.Write($"{i + 1} ")

            For j As Integer = 0 To BOARD_WIDTH - 1
                Console.Write(board(i, j))

            Next
            Console.WriteLine()
        Next

    End Sub

    Sub InitBoard(ByVal board(,) As String)

        For i As Integer = 0 To BOARD_HEIGHT - 1
            For j As Integer = 0 To BOARD_WIDTH - 1

                board(i, j) = "# "

            Next
        Next

    End Sub


End Module
