UML = plantuml
TEX = xelatex
BIB = biber

all:	deploy 

uml:
	$(UML) uml/*.uml

biber: validate
	$(BIB) main

%.tex: biber uml
	$(TEX) $@
	$(TEX) $@

validate: 
	$(UML) -checkonly uml/*.uml
	$(TEX) -draftmode *.tex

deploy: main.tex

clean:
	@-rm chapters/*.aux *.aux *.bbl *.bcf *.blg *.log *.lot *.lof *.pdf *.out *.run.xml *.toc 2> /dev/null
