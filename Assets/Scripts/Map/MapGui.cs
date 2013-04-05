using UnityEngine;
using System.Collections;

public class MapGui : MonoBehaviour {

    public GUISkin gSkin;
    public Location current_location;

    //public Texture2D circleDiagram;
    //public Texture2D starColor;
    //public Object starDiagram;
    Vector2 scrollPos;

    /*private Vector2 starCenter = new Vector2(64, 63);
    private Vector2 starTip1 = new Vector2(64, 2);
    private Vector2 starTip2 = new Vector2(121, 42);
    private Vector2 starTip3 = new Vector2(103, 110);
    private Vector2 starTip4 = new Vector2(24, 110);
    private Vector2 starTip5 = new Vector2(6, 42);
    private int counter;*/

    void Map_Main()
    {
        
        GUI.BeginGroup(new Rect(Screen.width - 400 , Screen.height - 500, Screen.width, Screen.height));
        GUI.Box(new Rect(0, 0, 350, 200), "");

        if (current_location != null)
        {
            GUI.Label(new Rect(0, 0, 350, 50), current_location.name);
            Vector2 sizeOfLabel = GUI.skin.GetStyle("Label").CalcSize(new GUIContent(current_location.description));
            if (sizeOfLabel.y > 150)
            {
                scrollPos = GUI.BeginScrollView(new Rect(0, 50, 350, 150), scrollPos, new Rect(0, 0, 0, sizeOfLabel.y), false, true);
                GUI.Label(new Rect(15, 0, 335, sizeOfLabel.y), current_location.description);
                GUI.EndScrollView();
            }
            else
                GUI.Label(new Rect(15, 50, 335, sizeOfLabel.y), current_location.description);
        }
        GUI.Box(new Rect(0, 200, 350, 250), "");
        //DrawStar(5, 4, 3, 2, 1);
        GUI.Label(new Rect(100 + 40, 215, 128, 128), "Difficulty:");
        GUI.Label(new Rect(100 + 15, 215 + 1 * 20, 128, 128), "Soldiers:");
        GUI.Label(new Rect(100 + 15, 215 + 2 * 20, 128, 128), "Length:");
        GUI.Label(new Rect(100 + 15, 215 + 3 * 20, 128, 128), "Pits:");
        GUI.Label(new Rect(100 + 15, 215 + 4 * 20, 128, 128), "Obstacles:");
        GUI.Label(new Rect(100 + 15, 215 + 5 * 20, 128, 128), "Catapults:");
        if (current_location != null)
        {
            GUI.Label(new Rect(100 + 100, 215 + 1 * 20, 128, 128), "" + current_location.difficulty_soldier);
            GUI.Label(new Rect(100 + 100, 215 + 2 * 20, 128, 128), "" + current_location.difficulty_length);
            GUI.Label(new Rect(100 + 100, 215 + 3 * 20, 128, 128), "" + current_location.difficulty_pits);
            GUI.Label(new Rect(100 + 100, 215 + 4 * 20, 128, 128), "" + current_location.difficulty_obstacles);
            GUI.Label(new Rect(100 + 100, 215 + 5 * 20, 128, 128), "" + current_location.difficulty_catapults);

            GUI.Label(new Rect(100 + 40, 215 + 7 * 20, 128, 128), "You gain:");
            GUI.Label(new Rect(100 + 15, 215 + 8 * 20, 128, 128), "+ " + current_location.gains);
            GUI.Label(new Rect(100 + 15, 215 + 9 * 20, 128, 128), "- " + current_location.losses);
            if (GUI.Button(new Rect(200, 215 + 10 * 20, 80, 30), "to battle!"))
            {
                ObstacleController.SOLDIER_RATIO = (int)current_location.difficulty_soldier;
                Application.LoadLevel(2);
            }
        }
        GUI.EndGroup();
    }
    /*
    Vector2 DeterminePoints(Vector2 tip, float size) {
        return (size - 5) * starCenter / 5 + size * tip / 5;
    }

    void DrawStar(float size1, float size2, float size3, float size4, float size5)
    {
        Vector2 point1 = DeterminePoints(starTip1, size1);
        Vector2 point2 = DeterminePoints(starTip2, size2);
        Vector2 point3 = DeterminePoints(starTip3, size3);
        Vector2 point4 = DeterminePoints(starTip4, size4);
        Vector2 point5 = DeterminePoints(starTip5, size5);
        counter = 0;
        TriangleRasterize(point1, point2, starCenter);
        //TriangleRasterize(point2, point3, starCenter);
        //TriangleRasterize(point3, point4, starCenter);
        //TriangleRasterize(point4, point5, starCenter);
        //TriangleRasterize(point5, point1, starCenter);
        print("pixels: " + counter);
    }

    // Draws a triangle. Cuts the triangle into two parts, horizontally.
    // The triangle is sorted, so that A has the lowest y-value, followed
    // by B and C. As such, A-C will always be the longest vertical
    // line, and is used as the longest line for the scaline triangle rasterization.
    void TriangleRasterize(Vector2 pointA, Vector2 pointB, Vector2 pointC)
    {
        Vector2 temp;

        // Sort the triangle so that for y, a<b<c
        if (pointC.y < pointB.y) {
            temp = pointC;
            pointC = pointB;
            pointB = temp;
        }
        if (pointB.y < pointA.y)
        {
            temp = pointB;
            pointB = pointA;
            pointA = temp;
        }
        if (pointC.y < pointB.y)
        {
            temp = pointC;
            pointC = pointB;
            pointB = temp;
        }

        // If triangle is flat, ignore
        if (pointA.y == pointC.y)
            return;
        DrawSpans(pointA, pointC, pointA, pointB);
        DrawSpans(pointA, pointC, pointB, pointC);
    }

    // Determines the individual horizontal spans to draw, one for
    // each y-value. Uses the scan line principle, and this method
    // determines the start and end point of the scanline per y-value.
    // LE1: The starting (top-most) point of the long edge
    // LE2: The ending (bottom-most) point of the long edge
    // SE1: The starting (top-most) point of the short edge
    // SE2: The ending (bottom-most) point of the short edge
    void DrawSpans(Vector2 LE1, Vector2 LE2, Vector2 SE1, Vector2 SE2)
    {
	    float dx1 = LE2.x-LE1.x, dx2 = SE2.x-SE1.x;			                            // Determine the delta x values for the two edges
	    float dy1 = LE2.y-LE1.y, dy2 = SE2.y-SE1.y;		                                // Determine the delta y values for the two edges
	    float f1 = (SE1.y-LE1.y)/dy1, fs1 = 1/dy1;							        	// Determine the factor and step increment for the short edge
	    if (dy2 == 0)																	// If the short edge is horizontal, draw only the line
		    return;
	    float f2 = 0, fs2 = 1/dy2;														// Determine it also factor for the long edge - always starts at top 
	    int y = (int)SE1.y;			    												// Start from the top of the short edge (assumes points are integers initially)
	    if (y < 0)																		// If it starts outside the screen, move down
		    y = 0;
        while (y <= SE2.y && y < Screen.height)                                         // Until lowest point of the short edge or screen bottom is reached
        {
            counter++;
            DrawSpan((int)(LE1.x + dx1 * f1 + 0.5), (int)(SE1.x + dx2 * f2 + 0.5), y);	// Draw an individual horizontal span.
		    f1 += fs1;																	// Increase the factors by a step
		    f2 += fs2;
		    y++;																		// Move down a single pixel
	    }
    }

    // Draw an individual span for the triangle rasterization.
    // Draws a horizontal line between two points in a triangle,
    // and Normalizes the vector normal average between them.
    void DrawSpan(int x1, int x2, int y)
    {
        if (x2 < x1)
        {
            int temp = x1;                                                  // Swaps the points if the second point
            x1 = x2;            											// is before the first. Important, as the algorithm
            x2 = temp;              										// only moves right.
        }
        int dx = x2 - x1;			        								// Find delta x
        if (dx == 0)														// If there is only one point, ignore.
            return;
        float f = 0, fs = 1 / ((float)dx);									// Determine the factor and step increment
        int x = x1;											        		// Start iteration from point one
        if (x < 0)															// if point one is before zero, move to zero.
            x = 0;
        while (x <= x2 && x < Screen.width)
        {						                                            // Until end point or screen width is reached...
            GUI.DrawTexture(new Rect(x+64, y+64, 1, 1), starColor);         // Draw the pixel
            f += fs;														// Increment factor by one step
            x++;															// move point to the right;
        }
    }
    */
    void OnGUI()
    {
        GUI.skin = gSkin;
        Map_Main();
    }

    public void ResetScroll()
    {
        scrollPos = Vector2.zero;
    }

    void Start()
    {
        ResetScroll();
    }
}
