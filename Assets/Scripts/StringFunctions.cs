﻿using UnityEngine;
using System.Collections;

public class StringFunctions : MonoBehaviour
{

		// lineWrap
		//  Splits string into lines which are _charsPerLine or shorter.
		//
		//  Current word is buffered so that if _charsPerLine is reached in the middle
		//  of a word the entire word appears on the next line.
		//
		//  Note that variable width fonts are not accounted for so it is likely
		//  you will have to set _charsPerLine much shorter than desired so that lines
		//  with several capital letters or wide chars wrap correctly.
		//
		//  Also takes carriage returns (\n) already in the string into account.
		//
		//
		// Parameters:
		//  string _str      - string to process
		//  int _charPerLine - max # of characters per line.
		//  bool _forceWrap  - if set to true, a continuous string of characters with no spaces
		//						word will be forced to wrap to _charsPerLine 
		//						if set to false, word will stay intact and violate _charsPerLine
		//
		// TODO:
		//	Don't count the space at end of a line.
		//  _forceWrap can cause somewhat odd behavior as it is a very simple implementation.
		//
		//  Provided by typeRice - June 12, 2009
		public static string lineWrap (string _str, int _charsPerLine, bool _forceWrap)
		{
				if (_str.Length < _charsPerLine)
						return _str;
		
		
				string result = "",
				buf = "";						// Stores current word
		
				int cursor = 0, // Position within _str
				charCount = 0;					// # of chars on current line
		
				bool bLineEmpty = false;				// if a new line was added to the result &
				// buf has not been appended.
		
				while (cursor < _str.Length) {
						buf += _str [cursor];				// add next character to buffer
						charCount++;						// increment count of chars on current line
			
						if (_str [cursor] == ' ') {			// if space is encountered
								result += buf;					// write the buffer to the result and clear
								buf = "";
								bLineEmpty = false;				// Something added since the last carriage return
						} else if (_str [cursor] == '\n') {		// manual carriage return encountered
								result += buf;					// write the buffer to the result and clear (buf contains the \n already)
								buf = "";
								charCount = 0;					// Start new line so reset character count.
						}
			
						if (charCount >= _charsPerLine) { 	// if charCount has reached max chars per line
								if (!bLineEmpty) {				// If line has something in it.
										result += '\n';				// Start a new line in the result
										charCount = buf.Length;		// reset character count to current buf size as ths will be placed on the new line.
										bLineEmpty = true;			// Newest line is empty
								} else if (_forceWrap) {			// else if the line is empty and we want to force word to wrap
										result += buf + '\n';		// write the buffer to the result with a carriage return
										buf = "";					// clear the buffer
										bLineEmpty = true;			// Newest line is empty
										charCount = 0;				// Start new line so reset character count.
								}
						}
			
						cursor++;							// Goto next character
				}
		
				result += buf;							// add any remaining characters in the buffer
				return result;
		}
}
