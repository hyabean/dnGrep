import sys
import pdfplumber
import time

filename = sys.argv[1]
destfilename = sys.argv[2]
# print("Filename: " + filename)

with open(destfilename, "w", encoding="utf-8") as file1:
    with pdfplumber.open(filename) as pdf:
        for page in pdf.pages:
            text = page.extract_text()
            file1.write(text)

