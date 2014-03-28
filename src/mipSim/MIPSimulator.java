package mipSim;

import mipSim.constructs.Log;
import mipSim.constructs.Memory;

public class MIPSimulator {
	
	private static int START;
	private static int END;
	private static String INPUT_FILE;
	private static String OUTPUT_FILE;
	private static String MODE;
	
	private static Memory MAIN_MEMORY;
	
	/**
	 * Entry point of simulator.
	 * 
	 * Format: 
	 * MIPSimulator inputfilename outputfilename operation [-Tm:n] 
	 * 
	 * inputfilename - The file name of the binary input file.
	 * outputfilename - The file name to which to print the output.
	 * operation - Either"dis" or"sim" to specify disassembly or simulation.
	 * -Tm:n - Optional argument to specify the start (m) and end (n) cycles of simulation tracing output. Tracing should be done in a single-step fashion with the contents of registers and memory shown after every processor cycle. -T0:0 indicates that no tracing is to be performed; eliminating the argument specifies that every cycle is to be traced.
	 * 
	 * @param args	- The list of above arguments provided
	 */
	
	public static void main(String[] args) {
		
		switch (args.length) {
		case 3:
			INPUT_FILE = args[0];
			OUTPUT_FILE = args[1];
			MODE = args[2];
			break;
		case 4:
			INPUT_FILE = args[0];
			OUTPUT_FILE = args[1];	
			MODE = args[2];
			//TODO: add intervals
			break;
		default:
			System.out.println("Usage:  java MIPSimulator <inputfilename> <outputfilename> <operation> [Tm:n]");
			System.exit(1);
		}
		
		MAIN_MEMORY = new Memory(584);	//initialize PC to 584 as per requirements
		MAIN_MEMORY.read(INPUT_FILE);			
		
		switch (MODE) {
		case "dis": 
			MIPSimulator.disassemble();
			break;
		case "sim": 
			MIPSimulator.simulate();
			break;
		default:
			Log.add("Should never reach.  Problem in mode switching in main.");
		}
	}

	private static void simulate() {
		System.out.println("Starting simulator...");
	}

	private static void disassemble() {
		System.out.println("Starting disassembler...");
		MAIN_MEMORY.print();
	}
	
}
