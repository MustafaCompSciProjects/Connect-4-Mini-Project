Imports System
Imports System.Numerics
Imports System.Threading
Imports Utilities

Module Program
    Const BOARD_WIDTH As Integer = 7
    Const BOARD_HEIGHT As Integer = 6
    Const NUMBER_OF_PLAYERS As Integer = 2
    Const EMPTY_SLOT As String = "# "

    Dim players(NUMBER_OF_PLAYERS - 1) As String

    Sub Main()



        Dim playerSymbols(NUMBER_OF_PLAYERS - 1) As Char

        If IsNothing(players(0)) Then
            GetNames(players)
            Console.WriteLine($"Hello {players(0)} and {players(1)}, I am the Connect 4 program")
        End If
        Console.Clear()


        'System.Threading.Thread.Sleep(1000)

        Console.Clear()

        Dim mainMenuChoice As Integer

        'Get a valid option
        Do
            mainMenuChoice = GetIntInput($"Would you like to: {vbCrLf} 1. Play Game {vbCrLf} 2. Exit")
        Loop Until mainMenuChoice = 1 Or mainMenuChoice = 2

        'Carry out that option
        If mainMenuChoice = 1 Then
            PlayGame(playerSymbols)
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

    Sub PlayGame(ByRef playerSymbols() As Char)
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
            If Not IsInRange(placeColumn, 0, BOARD_WIDTH - 1) Then

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
                placeRow = i - 1
            End If
        Next

        Console.WriteLine($"Calling CheckForWin()")
        CheckForWin(symbol, board, placeColumn, placeRow, symbolList)

    End Sub

    Sub CheckForWin(ByVal symbol As Char, ByRef board(,) As String, ByRef recentX As Integer, ByRef recentY As Integer, ByVal playerList As String)

        'Use the recent coordinates to check each possible direction relative to the latest move. More efficicent.

        '== HORIZONTAL CHECK ==
        Dim xToCheck As Integer
        Dim xInARow As Integer = 1

        'Slots to the right
        For i As Integer = 1 To 3

            xToCheck = recentX + i
            If Not xToCheck < BOARD_WIDTH Then
                Exit For
            End If

            If Not board(recentY, xToCheck) = symbol & " " Then
                Exit For
            End If

            xInARow += 1

        Next


        'Slots to the left
        For i As Integer = 1 To 5

            xToCheck = recentX - i
            If Not xToCheck >= 0 Then
                Exit For
            End If

            If Not board(recentY, xToCheck) = symbol & " " Then
                Exit For
            End If

            xInARow += 1

        Next

        If xInARow >= 4 Then
            'WIN CONDITION
            Console.WriteLine($"{players(playerList.IndexOf(symbol))} has won")
            Thread.Sleep(1000)
            Main()
        End If

        '== VERTICAL CHECK ==
        Dim yToCheck As Integer
        Dim yInARow As Integer = 1

        While False
            'Slots above 
            For i As Integer = 1 To 3

                yToCheck = recentY + i
                If Not yToCheck < BOARD_HEIGHT Then
                    Console.WriteLine($"Failed check within board height")
                    Exit For
                End If

                If Not board(yToCheck, recentX) = symbol & " " Then
                    Console.WriteLine($"Failed check {yToCheck}")
                    Exit For
                End If

                yInARow += 1
                Console.WriteLine($"DEBUG: Checked {yToCheck}")

            Next
        End While 'Decativate since 4 in a row has gravity

        'Slots below
        For i As Integer = 1 To 5

            yToCheck = recentY - i
            If Not yToCheck >= 0 Then
                Console.WriteLine($"Failed check within board height")
                Exit For
            End If

            If Not board(yToCheck, recentX) = symbol & " " Then
                Console.WriteLine($"Failed check {yToCheck}")
                Exit For
            End If

            yInARow += 1
            Console.WriteLine($"DEBUG: Checked {yToCheck}")

            Console.WriteLine($"DEBUG: Y in a row is {yInARow}")
        Next

        If yInARow >= 4 Then
            'WIN CONDITION
            Console.WriteLine($"{players(playerList.IndexOf(symbol))} has won")
            Thread.Sleep(1000)
            Main()
        End If

        '== Y=X ==




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
