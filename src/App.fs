module App

open Browser.Dom
open Fable.Core
open Fable.Core.JsInterop
open Cytoscape


let container =
    document.querySelector(".container") :?> Browser.Types.HTMLElement

let createNodeDefinition (id : string) (name : string) =
    let data =
        createEmpty<Cytoscape.NodeDataDefinition>

    data.["id"] <- Some (box id)
    data.["name"] <- Some (box name)

    jsOptions<Cytoscape.NodeDefinition>(fun o ->
        o.data <- data
    )

let createEdgeDefinition (source : string) (target : string) =
    let data =
        createEmpty<Cytoscape.EdgeDataDefinition>

    data.["source"] <- Some (box source)
    data.["target"] <- Some (box target)

    jsOptions<Cytoscape.EdgeDefinition>(fun o ->
        o.data <- data
    )

let elements =
    jsOptions<Cytoscape.ElementsDefinition>(fun o ->
        o.nodes <-
            ResizeArray [
                createNodeDefinition "j" "J"
                createNodeDefinition "e" "E"
                createNodeDefinition "k" "K"
                createNodeDefinition "g" "G"
            ]
        o.edges <-
            ResizeArray [
                createEdgeDefinition "j" "e"
                createEdgeDefinition "j" "k"
                createEdgeDefinition "j" "g"
                createEdgeDefinition "e" "j"
                createEdgeDefinition "e" "k"
                createEdgeDefinition "k" "j"
                createEdgeDefinition "k" "e"
                createEdgeDefinition "k" "g"
                createEdgeDefinition "g" "j"
            ]
    )

cytoscape.cytoscape(jsOptions<Cytoscape.CytoscapeOptions>(fun o ->
    o.container <- Some container
    o.elements <- Some (U4.Case1 elements)
    o.layout <- Some (box
        {|
            name = "grid"
            padding = 10
        |}
    )
))
|> ignore
