UML = plantuml
TEX = xelatex
BIB = biber

.PHONY: uml biber

all:	deploy 

uml:
	$(UML) uml/*.uml

biber: validate
	$(BIB) main

%.tex: biber
	$(TEX) $@
	$(TEX) $@

validate: 
	$(TEX) -draftmode *.tex

deploy: main.tex
	gs -sDEVICE=pdfwrite -dCompatibilityLevel=1.4 -dPDFSETTINGS=/prepress -dNOPAUSE -dQUIET -dBATCH -sOutputFile=output_`git branch --show-current`.pdf main.pdf

clean:
	@-rm chapters/*.aux *.aux *.bbl *.bcf *.blg *.log *.lot *.lof *.pdf *.out *.run.xml *.toc 2> /dev/null
