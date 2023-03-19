UML = plantuml
TEX = pdflatex

all:	deploy 

uml:
	$(UML) uml/*.uml

%.tex: uml
	$(TEX) $@

validate: 
	$(UML) -checkonly uml/*.uml
	$(TEX) -draftmode *.tex

deploy: main.tex
