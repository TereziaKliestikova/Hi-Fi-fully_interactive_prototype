export type Annotation = {
  id: number;
  name: string;
  displayedName: string; // This holds the actual name to display (bcs of hiding name)
  description: string;
  coords: {
    minX: number;
    minY: number;
    maxX: number;
    maxY: number;
  };
  visible: boolean; // Property to manage visibility of annotation
};
