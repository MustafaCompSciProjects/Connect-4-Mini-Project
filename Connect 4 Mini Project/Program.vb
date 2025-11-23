Imports System
Imports System.Numerics
Imports System.Threading
Imports Utilities

Module Program
    Const BOARD_WIDTH As Integer = 7
    Const BOARD_HEIGHT As Integer = 6
    Const NUMBER_OF_PLAYERS As Integer = 2
    Const EMPTY_SLOT As String = "# "


    Sub Main(args As String())


        Dim players(NUMBER_OF_PLAYERS - 1) As String
        Dim playerSymbols(NUMBER_OF_PLAYERS - 1) As Char

        GetNames(players)

        Console.Clear()
        Console.WriteLine($"Hello {players(0)} and {players(1)}, I am the Connect 4 program")

        'System.Threading.Thread.Sleep(1000)

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
        Dim gameBoard(BOARD_HEIGHT - 1, BOARD_WIDTH - 1) As String

        PickSymbols(playerSymbols)

        InitBoard(gameBoard)

        PrintBoard(gameBoard)

        Do
            For Each symbol In playerSymbols
                PlaceCounter(symbol, gameBoard, playerSymbols)
                'Console.Clear()
                PrintBoard(gameBoard)
                If IsBoardFull(gameBoard) Then
                    Console.WriteLine($"The board is full, its a tie!")
                    Exit Do
                End If
            Next
        Loop
    End Sub
    Function IsBoardFull(board(,) As String)

        For row As Integer = 0 To BOARD_HEIGHT - 1

            For col As Integer = 0 To BOARD_WIDTH - 1

                If board(row, col) = EMPTY_SLOT Then
                    Return False
                End If

            Next
        Next
        Return True
    End Function
    Sub PlaceCounter(ByVal symbol As Char, ByRef board(,) As String, ByVal symbolList() As Char)
        'Get and validate the column that the player wants to move in
        Dim placeColumn As Integer
        Dim placeRow As Integer

        Do
            placeColumn = GetIntInput($"What column would you like to place it in? (1-{BOARD_WIDTH})") - 1
            'Ensure desired move is within range and is available
            If Not IsInRange(placeColumn, BOARD_WIDTH - 1, 0) Then

            ElseIf Not board(BOARD_HEIGHT - 1, placeColumn) = EMPTY_SLOT Then

            Else
                Exit Do
            End If

            Console.WriteLine($"Please select a slot from 1-{BOARD_WIDTH} that is available")
        Loop

        'Check slots below the placed location and move down if possible
        For i As Integer = BOARD_HEIGHT - 1 To 1 Step -1

            If board(i - 1, placeColumn) = EMPTY_SLOT Then
                'If the spot below is available, then place in that spot and remove from above
                board(i - 1, placeColumn) = symbol & " "
                board(i, placeColumn) = EMPTY_SLOT
            End If
        Next

        CheckForWin(symbol, board, placeColumn, placeRow)

    End Sub

    Sub CheckForWin(ByVal symbol As Char, ByRef board(,) As String, ByRef recentX As Integer, ByRef recentY As Integer)

        'Use the recent coordinates to check each possible direction relative to the latest move. More efficicent.

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
        For i As Integer = BOARD_HEIGHT - 1 To 0 Step -1
            Console.Write($"{i + 1} ")

            For j As Integer = 0 To BOARD_WIDTH - 1

                Console.Write(board(i, j))

            Next
            Console.WriteLine()
        Next

    End Sub

    Sub InitBoard(ByVal board(,) As String)
        For row As Integer = 0 To BOARD_HEIGHT - 1
            For col As Integer = 0 To BOARD_WIDTH - 1
                board(row, col) = EMPTY_SLOT
            Next
        Next

    End Sub


End Module
